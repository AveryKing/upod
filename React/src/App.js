import React, {useEffect, useState} from "react";

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
   <div style={{textAlign:'center',marginTop:'25vh'}}>
    <h2>To-Do List</h2>
     <h3>This is a simple to-do list built with ASP.NET Core.</h3>

       {list.map(item => (
           <p key={item.id}>{item.name}</p>
       ))}

   </div>
  );
}

export default App;
