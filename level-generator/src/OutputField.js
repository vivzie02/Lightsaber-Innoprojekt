import { TextField } from '@mui/material';
import './OutputField.css';
import React from "react";

function OutputField(){
    return (
        <div className='OutputField'>
            <h2>Level Parts: </h2><br></br>
        
            <div id='levelParts'>
                
            </div>
        </div>
    )
}

export function addLevelPart(allLevelParts){
    const parent = document.getElementById("levelParts");
    parent.innerHTML = ""

    allLevelParts.forEach(element => {
        
        const sentenceDiv = document.createElement("div");
        sentenceDiv.className = "levelList";
        sentenceDiv.textContent = element.sentence;

        
        console.log(sentenceDiv);

        parent.appendChild(sentenceDiv);
        
    });
}

export default OutputField;

