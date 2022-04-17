import React, {useEffect, useState} from 'react';
import tasks from '../services/tasks';
import {Badge, Box, Flex, Grid, GridItem, SimpleGrid, Text} from "@chakra-ui/react";
import TaskCard from "./TaskCard";

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
        <Flex direction='column'>
        <Text
            fontSize='2xl'
            marginTop={{sm:10,md:18,lg:20}}
            marginLeft={{sm:10,md:18,lg:20}} align='start' justifyContent='end'>Your Tasks</Text>
            <Text
                marginTop={5}
                marginBottom={-2}
                marginLeft={{sm:10,md:18,lg:20}} fontSize='xl' fontWeight='bold'>
                <Badge ml='1' fontSize='0.7em' colorScheme='red'>
                    Incomplete
                </Badge>
            </Text>
            <SimpleGrid
                marginLeft={{sm:10,md:18,lg:20}}
                marginTop={5}
                marginRight={{sm:10,md:18,lg:20}}
                columns={{sm:2, md:3, lg:4}} gap={6}>

            {tasksList.map((task,index) => (
                <TaskCard key={index} task={task} />
            ))}
            </SimpleGrid>
            </Flex>


    );
};

export default TaskList;