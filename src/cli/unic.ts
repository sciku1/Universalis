import chalk from "chalk";
import fs from "fs";
import { MongoClient } from "mongodb";
import readline from "readline";
import util from "util";

import { TrustedSourceManager } from "../TrustedSourceManager";

import { CLIResources } from "../models/CLIResources";

const readdir = util.promisify(fs.readdir);

// Load resources
const db = MongoClient.connect("mongodb://localhost:27017/", { useNewUrlParser: true, useUnifiedTopology: true });
var trustedSources: TrustedSourceManager;

const commands: Map<string, Function> = new Map();

var resources: CLIResources;

const init = (async () => {
    const universalisDB = (await db).db("universalis");

    trustedSources = await TrustedSourceManager.create(universalisDB);

    const commandFiles = await readdir("./commands");
    commandFiles.forEach((fileName) => {
        commands.set(fileName.substr(0, fileName.indexOf(".")), require(`./commands/${fileName}`));
    });

    resources = {
        trustedSources
    };
})();

// Console application
console.log(chalk.cyan(`Universalis Console Tool v${require("../../package.json").unicVersion}`));

const stdin = readline.createInterface({
    completer: autocomplete,
    input: process.stdin,
    output: process.stdout,
    prompt: chalk.cyan("> ")
});

stdin.prompt();

stdin.on("line", async (line) => {
    await init;

    const args = line.split(/\s+/g);
    const command = args.pop();

    const commandF = commands.get(command);
    if (commandF) {
        commandF(resources, args);
    } else {
        console.log(chalk.bgYellow.black(`'${command}' is not a valid command.`));
    }

    stdin.prompt();
}).on("close", () => {
    console.log(chalk.cyan("\nGoodbye."));
    process.exit(0);
});

function autocomplete(line: string) {
    const completions = ["addkey"];
    const hits = completions.filter((command) => line.startsWith(command));
    return [hits.length ? hits : completions, line];
}
