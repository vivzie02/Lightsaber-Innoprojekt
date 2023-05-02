import './InputField.css';
import React, { useState } from "react";
import { TextField } from "@mui/material";
import Button from '@mui/material/Button'
import List from '@mui/material/List';
import ListItem from '@mui/material/ListItem';
import ListItemText from '@mui/material/ListItemText';
import ListItemAvatar from '@mui/material/ListItemAvatar';
import Avatar from '@mui/material/Avatar';
import { FaCube } from 'react-icons/fa'
import { LevelPart } from './LevelPart';
import { addLevelPart } from './OutputField'; 
import { red, yellow } from '@mui/material/colors';
import { upload } from './LevelUpload';


function InputField(){
    const [allLevelParts, setAllLevelParts] = useState([]);

    const [list, setList] = useState([
        
    ]);

    const addBlock = () => {
        var blockText = document.getElementById("blockInput").value;
        if(blockText == ""){
            alert("Please input a value for the block");
            return; 
        }

        const newItem = { blockText: blockText, icon: <Avatar><FaCube /></Avatar>, correct: false, bgcolor: 'red' };
        setList([...list, newItem]);

        document.getElementById("blockInput").value = "";
    }

    const selectBlock = (index) => {
        const newList = list.map((item, i) => {
            if(i === index){
                return {...item, correct: true, bgcolor: 'green'}
            } else{
                return {...item, correct: false, bgcolor: 'red'}
            }
        });
        setList(newList);
    }

    const saveEntry = () => {
        var sentence = document.getElementById("sentence").value;
        if(sentence == ""){
            alert("Please enter a sentence first!");
            return;
        }

        var blocks = [];
        var correctBlock;

        const blockList = list.map((item, i) => {
            if(item.correct){
                correctBlock = i;
            }
            blocks.push(item.blockText);
        });
        if(blockList.length == 0){
            alert("Please assign at least one block to the level first");
            return;
        }

        if(correctBlock == null){
            alert("Please select the correct Block");
            return;
        }

        const entry = new LevelPart(sentence, blocks, correctBlock);

        setAllLevelParts([...allLevelParts, entry]);

        addLevelPart([...allLevelParts, entry]);

        setList([]);
    }

    const exportData = () => {
        
        //LevelUpload.js
        upload(allLevelParts);
        
    }

    return(
        <div className="InputField">
            <TextField id="sentence" label="Sentence" variant="outlined"
                sx={{
                    margin: 2,
                    width: '60%'

              }}/><br/>
            <TextField id="blockInput" label="Block" variant="outlined"
                sx={{
                    margin: 2
                }}/>
  
            <Button variant="contained" onClick={addBlock}
                sx={{
                    margin: 2
                }}>
                Add Block
            </Button>

            <Button variant="contained" color="primary" sx={{backgroundColor: yellow[800]}} onClick={() => saveEntry()}>Save Entries</Button>

            <Button variant='contained' color="primary" sx={{backgroundColor: red[500], margin: 2}} onClick={() => exportData()}>Export Level</Button>

            <div id='blocks'>
                <List sx={{ width: '100%', maxWidth: 360 }}>
                    {list.map((item, index) => (
                    <ListItem key={index} className='block' onClick={() => selectBlock(index)} style={{backgroundColor: item.bgcolor}}>
                        <ListItemAvatar>
                        <Avatar>
                            {item.icon}
                        </Avatar>
                        </ListItemAvatar>
                        <ListItemText primary={item.blockText} />
                    </ListItem>
                    ))}
                </List>
            </div>
        </div>
        
    )
}

export default InputField;