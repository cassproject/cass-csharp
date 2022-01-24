require("cassproject");

let repo = new EcRepository();

let goGetSomeData = async function () {
    await repo.init("https://dev.cassproject.org/api/");
    return JSON.stringify(await EcFramework.search(repo, "*"));
}

module.exports = {
    goGetSomeData
}