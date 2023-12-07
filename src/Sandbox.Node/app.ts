import readline from "readline";

var reader = readline.createInterface({
    input: process.stdin,
    output: process.stdout,
});

console.log('Hello world');


function add<T extends number>(a: T, b: T): T {
    return (a + b) as T;
}

var weirdResult = add(5, 5);

console.log(weirdResult);

reader.question("What is this?", answer => {
    console.log(`This is a ${answer}`);
    reader.close();
});

