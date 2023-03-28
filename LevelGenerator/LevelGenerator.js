function addBlock(){
    const breaker = document.createElement("br")
    const block = document.createElement("input");
    block.type = "text";

    if(document.getElementById("blockInput").value == ""){
        alert("No Block text given");
        return;
    }
    block.value = document.getElementById("blockInput").value;
    document.getElementById("blockInput").value = "";

    block.className = "block";

    document.getElementById("blockList").appendChild(block);
    document.getElementById("blockList").appendChild(breaker);
}


function generateJson(){
    const sentence = document.getElementById("sentence").value;
    const correctBlock = document.getElementById("number").value;
    const blocks = [];

    const blockList = document.getElementById("blockList");

    for(let i = 0; i < blockList.childNodes.length; i++){
        blocks.push(blockList.childNodes[i].value);
    }

    const jsonData = {
        sentence,
        blocks,
        correctBlock
    };


    document.getElementById("jsonField").value = document.getElementById("jsonField").value.slice(0, -1);
    if(document.getElementById("jsonField").value != "["){
        document.getElementById("jsonField").value += ",\n"
    }
    
    document.getElementById("jsonField").value += JSON.stringify(jsonData);
    document.getElementById("jsonField").value += "\n]";
}

function downloadJson(){
    const textArea = document.getElementById("jsonField");
    const jsonString = textArea.value;
    const jsonObject = JSON.parse(jsonString);

    getFile(jsonObject, "Generated_Level");
}

function getFile(jsonObject, name){
    const json = JSON.stringify(jsonObject);
    const blob = new Blob([json], {type: "application/json"});

    const url = URL.createObjectURL(blob);
  
    const link = document.createElement("a");
    link.href = url;
    link.download = name;
    
    document.body.appendChild(link);
    link.click();
    
    document.body.removeChild(link);
    URL.revokeObjectURL(url);
}