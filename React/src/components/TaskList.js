import React, {useEffect, useState} from 'react';
import tasks from '../services/tasks';

const TaskList = ({user}) => {
    const [tasksList, setTasksList] = useState([]);
    useEffect(() => {
        if(!user) {
            alert('Please login to view your tasks');
        } else {
            tasks.getAll(user.token)
                .then(res => {
                    setTasksList(res);
                })
        }
    }, [tasks,user])
    return (
        <div>
            {tasksList.map((task,index) => (
                <p key={index}>New task!</p>
            ))}
        </div>
    );
};

export default TaskList;