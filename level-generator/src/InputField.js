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
import { NestCamWiredStand } from '@mui/icons-material';


function InputField(){
    const [allLevelPartsLeft, setAllLevelPartsLeft] = useState([]);
    const [allLevelPartsRight, setAllLevelPartsRight] = useState([]);
    const [listLeft, setListLeft] = useState([]);
    const [listRight, setListRight] = useState([]);

    const addBlock = (direction) => {
        var blockText = document.getElementById("blockInput").value;
        if(blockText == ""){
            alert("Please input a value for the block");
            return; 
        }

        const newItem = { blockText: blockText, icon: <Avatar><FaCube /></Avatar>, correct: false, bgcolor: 'red' };
        
        if(direction === "left"){
            setListLeft([...listLeft, newItem]);
        }
        else{
            setListRight([...listRight, newItem]);
        }
        document.getElementById("blockInput").value = "";
    }

    const addBlockLeft = () => {
        addBlock("left");
    }

    const addBlockRight = () => {
        addBlock("right");
    }

    const selectBlock = (index, direction) => {
        if(direction === "left"){
            const newList = listLeft.map((item, i) => {
                if(i === index){
                    return {...item, correct: true, bgcolor: 'green'}
                } else{
                    return {...item, correct: false, bgcolor: 'red'}
                }
            });
            setListLeft(newList);

            const newRightList = listRight.map((item, i) => {
                return {...item, correct: false, bgcolor: 'red'}
            });
            setListRight(newRightList);
        }
        else{
            const newList = listRight.map((item, i) => {
                if(i === index){
                    return {...item, correct: true, bgcolor: 'green'}
                } else{
                    return {...item, correct: false, bgcolor: 'red'}
                }
            });
            setListRight(newList);

            const newLeftList = listLeft.map((item, i) => {
                return {...item, correct: false, bgcolor: 'red'}
            });
            setListLeft(newLeftList);
        }
    }

    const saveEntry = () => {
        var sentence = document.getElementById("sentence").value;
        if(sentence == ""){
            alert("Please enter a sentence first!");
            return;
        }

        var blocks = [];
        var correctBlock;

        const blockListLeft = listLeft.map((item, i) => {
            if(item.correct){
                correctBlock = i;
            }
            blocks.push({
                text: item.blockText,
                position: "left"
            });
        });
        const blockListRight = listRight.map((item, i) => {
            if(item.correct){
                correctBlock = blockListLeft.length + i;
            }
            blocks.push({
                text: item.blockText,
                position: "right"
            });
        });

        if(blockListLeft.length === 0 && blockListRight === 0){
            alert("Please assign at least one block to the level first");
            return;
        }

        if(correctBlock == null){
            alert("Please select the correct Block");
            return;
        }

        const entry = new LevelPart(sentence, blocks, correctBlock);

        setAllLevelPartsLeft([...allLevelPartsLeft, entry]);

        addLevelPart([...allLevelPartsLeft, entry]);

        setListLeft([]);
        setListRight([]);
    }

    const exportData = () => {
        
        //LevelUpload.js
        var fileName = document.getElementById("fileName").value;

        const fileNameRegex = new RegExp("^[^\.<>:\"\/\\|?*]+$");
        if(!fileNameRegex.test(fileName)){
            alert("Input a valid Level Name");
            return;
        }

        upload(allLevelPartsLeft, fileName);
        
    }

    return(
        <div className="InputField">
            <TextField id="fileName" label="fileName" variant="outlined"
                sx={{
                    margin: 2,
                    width: '60%'

              }}/><br/>
            <TextField id="sentence" label="Sentence" variant="outlined"
                sx={{
                    margin: 2,
                    width: '60%'

              }}/><br/>
            <TextField id="blockInput" label="Block" variant="outlined"
                sx={{
                    margin: 2
                }}/>
  
            <Button variant="contained" onClick={addBlockLeft}
                sx={{
                    margin: 2
                }}>
                Add Block Left
            </Button>

            <Button variant="contained" onClick={addBlockRight}
                sx={{
                    margin: 2
                }}>
                Add Block Right
            </Button>

            <Button variant="contained" color="primary" sx={{backgroundColor: yellow[800]}} onClick={() => saveEntry()}>Save Entries</Button>

            <Button variant='contained' color="primary" sx={{backgroundColor: red[500], margin: 2}} onClick={() => exportData()}>Export Level</Button>

            <div id='blocks' style={{ display: 'flex' }}>
                <div id='left' style={{ flex: 1 }}>
                    <List sx={{ width: '100%', maxWidth: 360 }}>
                        {listLeft.map((item, index) => (
                            <ListItem key={index} className='block' onClick={() => selectBlock(index, "left")} style={{ backgroundColor: item.bgcolor }}>
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
                <div id='right' style={{ flex: 1 }}>
                    <List sx={{ width: '100%', maxWidth: 360 }}>
                        {listRight.map((item, index) => (
                            <ListItem key={index} className='block' onClick={() => selectBlock(index, "right")} style={{ backgroundColor: item.bgcolor }}>
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

        </div>
        
    )
}

export default InputField;