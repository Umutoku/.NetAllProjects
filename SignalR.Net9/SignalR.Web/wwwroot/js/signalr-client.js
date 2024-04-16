"use strict";

const broadcastMessageToAllClientHubMethodCall = "BroadcastMessageToAllClient";
const receiveMessageForAllClientClientMethodCall = "ReceiveMessageForAllClient";

const broadcastMessageToCallerClient = "BroadcastMessageToCallerClient";
const receiveMessageForCallerClient = "ReceiveMessageForCallerClient";


const broadcastMessageToOthersClient = "BroadcastMessageToOthersClient";
const receiveMessageForOthersClient = "ReceiveMessageForOthersClient";


const broadcastMessageToIndividualClient = "BroadcastMessageToIndividualClient";
const receiveMessageForIndividualClient = "ReceiveMessageForIndividualClient";

const receiveConnectedClientCountAllClient = "ReceiveConnectedClientCountAllClient";

const receiveTypedMessageForAllClient = "ReceiveTypedMessageForAllClient"
const BroadcastTypedMessageToAllClient = "BroadcastTypedMessageToAllClient";

const groupA = "GroupA";
const groupB = "GroupB";
let currentGroupList = [];

const connection = new signalR.HubConnectionBuilder()
    .withUrl("/exampletypesafehub")
    .build();

function refreshGroupList() {
    const groupList = $("#group-list");
    groupList.empty();

    currentGroupList.forEach(group => {
        groupList.append(`<p>${group}</p>`);
    })
}

$("#btn-groupA-add").click(function () {
    if (currentGroupList.includes(groupA)) return;
    connection.invoke("JoinGroup", groupA).then(() => {
        currentGroupList.push(groupA);
        refreshGroupList();
    }).catch(err => console.error("hata", err))
})

$("#btn-groupB-add").click(function () {
    if (currentGroupList.includes(groupB)) return;
    connection.invoke("JoinGroup", groupB).then(() => {
        currentGroupList.push(groupB);
        refreshGroupList();
    }).catch(err => console.error("hata", err))
})

$("#btn-groupA-remove").click(function () {
    if (!currentGroupList.includes(groupA)) return;
    connection.invoke("LeaveGroup", groupA).then(() => {
        currentGroupList = currentGroupList.filter(group => group !== groupA);
        refreshGroupList();
    }).catch(err => console.error("hata", err))
})

$("#btn-groupB-remove").click(function () {
    if (!currentGroupList.includes(groupB)) return;
    connection.invoke("LeaveGroup", groupB).then(() => {
        currentGroupList = currentGroupList.filter(group => group !== groupB);
        refreshGroupList();
    }).catch(err => console.error("hata", err))
})

$("#btn-groupA-send-message").click(function () {
    const message = "Grup a"
    connection.invoke("BroadcastMessageToGroupClient", groupA, message).catch(err => console.error("hata", err))
})

$("#btn-groupB-send-message").click(function () {
    const message = "Grup b"
    connection.invoke("BroadcastMessageToGroupClient", groupB, message).catch(err => console.error("hata", err))
})
async function start() {

    try {
        await connection.start().then(() => {
            console.log("Hub ile bağlantı kuruldu");
            $("#connectionId").html(`Connection Id : ${connection.connectionId}`);
        });
    }
    catch (err) {
        console.error("hub ile bağlantı kurulamadı", err);
        setTimeout(() => start(), 3000)
    }
}

connection.on(receiveMessageForAllClientClientMethodCall, (message) => {
    console.log("Mesaj alındı", message);
})

connection.on(receiveMessageForCallerClient, (message) => {
    console.log("Mesaj alındı", message);
})

connection.on(receiveMessageForOthersClient, (message) => {
    console.log("Mesaj alındı", message);
})

connection.on(receiveMessageForIndividualClient, (message) => {
    console.log("Mesaj alındı", message);
})

connection.on("ReceiveMessageForGroupClient", (message) => {
    console.log("Mesaj alındı", message);
})

connection.on(receiveTypedMessageForAllClient, (message) => {
    console.log("Mesaj alındı", message);
})

connection.onclose(async () => {
    await start();
})

start();

const span_client_count = $("#span-connected-client-count");
connection.on(receiveConnectedClientCountAllClient, (count) => {
    span_client_count.text(count);
    console.log("connected client count", count);
})

$("#span-connection-id").text(`Connection Id: ${connection.connectionId}`)


$("#btn-send-message-all-client").click(function () {

    const message = "hello world";
    connection.invoke(broadcastMessageToAllClientHubMethodCall, message).catch(err => console.error("hata", err))
    console.log("Mesaj gönderildi.")
})

$("#btn-send-message-caller-client").click(function () {
    const message = "hello world";
    connection.invoke(broadcastMessageToCallerClient, message).catch(err => console.error("hata", err))
    console.log("Mesaj gönderildi.")
})

$("#btn-send-message-others-client").click(function () {
    const message = "hello world";
    connection.invoke(broadcastMessageToOthersClient, message).catch(err => console.error("hata", err))
    console.log("Mesaj gönderildi.")
})

$("#btn-send-message-individual-client").click(function () {
    const connectionId = $("#text-connectionId").val();
    const message = "hello world";
    connection.invoke(broadcastMessageToIndividualClient, connectionId, message).catch(err => console.error("hata", err))
    console.log("Mesaj gönderildi.")
})

$("#btn-send-typed-message-all-client").click(function () {
    const message = { id: "1", name: "kara", price: 25 };
    connection.invoke(BroadcastTypedMessageToAllClient, message).catch(err => console.error("hata", err))
    console.log("Mesaj gönderildi.")
})