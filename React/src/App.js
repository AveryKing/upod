import React, {useEffect, useState} from "react";
import {Box, Button, Checkbox, TextField, Typography} from "@mui/material";
import ToDoItem from "./components/ToDoItem";
import ItemTable from "./components/ItemTable";

const API_URL = 'https://localhost:7135/api/todo/';
function App() {
  const [list, setList] = useState([]);

  useEffect(() => {
    fetch(API_URL)
        .then(res => res.json())
        .then(data => {
          setList(data);
        })
  },[setList]);

  return (
   <div style={{textAlign:'center',marginTop:'10vh'}}>
    <Typography variant='h5'>Task List</Typography>
     <Typography variant='h6'>Enter an item below to add to the list</Typography>
       <Box sx={{marginBottom:'30px',display:'flex',alignItems:'center',justifyContent:'center'}}>
           <TextField color='secondary' id="outlined-basic" label="New Task" variant="standard" />
           <Button color='secondary' sx={{marginLeft:'5px',marginTop:'20px'}}>Add</Button>
       </Box>

       <ItemTable list={list} />

   </div>
  );
}

export default App;
