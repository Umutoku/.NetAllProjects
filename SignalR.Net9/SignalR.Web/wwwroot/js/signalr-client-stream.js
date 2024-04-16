"use strict";

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/exampletypesafehub")
    .build();

async function start() {
    try {
        await connection.start().then(() => {
            console.log("connected");
        }).catch(err => console.error(err));
    } catch {
        setTimeout(start(), 5000);
    }

}

connection.onclose(async () => {
    await start();
});

const broadcastMessageAsStreamToAllClient = "BroadcastMessageAsStreamToAllClient";
const receiveMessageAsStreamFromClient = "ReceiveMessageAsStreamFromClient";

const broadcastStreamProductToAllClient = "BroadcastStreamProductToAllClient";
const receiveStreamProductFromClient = "ReceiveStreamProductFromClient";

const broadcastCastFromToHubClient = "BroadcastCastFromToHubClient";

connection.on(receiveMessageAsStreamFromClient, (message) => {
    $("#streamBox").append(`<p>${message}</p>`);
});

connection.on(receiveStreamProductFromClient, (product) => {
    $("#streamBox").append(`<p>${product.name}</p>`);
});

$("#btn-from-client-to-hub").click(() => {
    const names = $("#txt-stream").val();
    const nameAsChunck = names.split(";");
    const subject = new signalR.Subject();
    connection.send(broadcastMessageAsStreamToAllClient, subject).catch(err => console.error(err));
    nameAsChunck.forEach(name => {
        subject.next(name);
    });
    subject.complete();
});

$("#btn-from-client-to-hub-product").click(() => {
    const products = [{ id: 1, name: "pen 1", price: 100 },
        { id: 2, name: "pen 2", price: 200}
    ]
    const subject = new signalR.Subject();
    connection.send(broadcastStreamProductToAllClient, subject).catch(err => console.error(err));
    productAsChunck.forEach(product => {
        subject.next(JSON.parse(product));
    });
    subject.complete();
});

$("#btn-from-hub-to-client").click(() => {
    connection.stream(broadcastCastFromToHubClient, 5).subscribe({
        next: (item) => {
            $("#streamBox").append(`<p>${item}</p>`);
        },
        complete: () => {
            console.log("complete");
        },
        error: (err) => {
            console.error(err);
        }
    });
});


start();

