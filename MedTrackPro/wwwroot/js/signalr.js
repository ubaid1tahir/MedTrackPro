
var connection = new signalR.HubConnectionBuilder().withUrl("/hub/client").build();
var inputMsg = document.getElementById('msg');
var receiverId = document.getElementById('receiverId');
var senderName = document.getElementById('senderName');
var sendBtn = document.getElementById('send-btn');


connection.on("OnSendMessage", (msg) => {
    console.log("This is message :", msg.text);
    const messageHTML = `
        <div class="row">
            <div class="col-md-6 text-start">
                <div class="card rounded-2 mt-3 p-2 bg-primary text-white shadow-sm">
                    <span class="">${msg.text}</span>
                </div>
            </div>
        </div>
    `;
    const parentDiv = document.getElementById("messages");
/*    parentDiv.insertAdjacentHTML("afterbegin", `<ul><li>${msg.text}</li></ul>`)*/

    //messages.push(messageHTML);
    parentDiv.insertAdjacentHTML("afterbegin", messageHTML);


})

function sendMessage() {
    //let userid = sessionStorage.getItem("userid");
    console.log("Id :", receiverId.value);
    if (inputMsg.value != "") {
        connection.invoke("SendMessage", inputMsg.value, receiverId.value, senderName.value).then((val) => {
            console.log(val);
        });
    }
    console.log("Input Value :", inputMsg.value);
    //const messageHTML = `
    //     <div class="row">
    //        <div class="offset-sm-6 col-sm-6 col-md-6 text-start">
    //            <div class="card rounded-2 mt-3 p-2 bg-secondary text-white shadow-sm">
    //                <span class="">${inputMsg.value}</span>
    //            </div >
    //        </div >

    //    </div > 
    //`;
    inputMsg.value = "";
    //messages.push(messageHTML);
    //parentDiv.insertAdjacentHTML("afterbegin", messageHTML);
}

function writeOnSenderScreen() {
    const messageHTML = `
         <div class="row">
            <div class="offset-sm-6 col-sm-6 col-md-6 text-start">
                <div class="card rounded-2 mt-3 p-2 bg-secondary text-white shadow-sm">
                    <span class="">${inputMsg.value}</span>
                </div >
            </div >

        </div >
    `;
    const parentDiv = document.getElementById("messages");
    parentDiv.insertAdjacentHTML("afterbegin", messageHTML);
}

connection.start().then(() => {
    console.log("Connection successfully made with the server");
    inputMsg.addEventListener('keydown', (event) => {
        if (event.key === "Enter") {
            sendBtn.click();
        }
    })
    sendBtn.addEventListener('click', () => {
        writeOnSenderScreen();
        console.log("You clicked me");
        sendMessage();

    })
})