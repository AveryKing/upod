import React, {useEffect, useState} from 'react';
import tasks from '../services/tasks';
import {Flex, Text} from "@chakra-ui/react";
import TaskGroup from "./TaskGroup";
import TaskFilters from "./TaskFilters";

const TaskList = ({user}) => {
    const [tasksList, setTasksList] = useState([]);
    useEffect(() => {
        if (!user) {
            alert('Please login to view your tasks');
        } else {
            tasks.getAll(user.token)
                .then(res => {
                    setTasksList(res);
                })
        }
    }, [tasks, user])

    return (
        <Flex direction='column'>
                <Text
                    fontSize='2xl'
                    mt={{sm: 10, md: 18, lg: 20}}
                    ml={{sm: 10, md: 18, lg: 20}}
                    align='start'>
                    Your Tasks
                </Text>
                <TaskFilters/>
                <TaskGroup
                    tasks={tasksList}
                    showBadge={true}
                    completed={false}
                />

        </Flex>
    );
};

export default TaskList;