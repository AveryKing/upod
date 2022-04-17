import React from 'react';
import {Badge, SimpleGrid, Text} from "@chakra-ui/react";
import TaskCard from "./TaskCard";

const TaskGroup = ({tasks, completed, showBadge}) => {
    return (
        <>
            {showBadge &&
                <Text mt={5} mb={-2} ml={{sm: 10, md: 18, lg: 20}} fontSize='xl' fontWeight='bold'>
                    <Badge borderRadius={6} ml='1' fontSize='0.7em' colorScheme={completed ? "green" : "red"}>
                        {completed ? "Complete" : "Incomplete"}
                    </Badge>
                </Text>
            }

            <SimpleGrid ml={{sm: 10, md: 18, lg: 20}} mt={5} mr={{sm: 10, md: 18, lg: 20}}
                        columns={{sm: 2, md: 3, lg: 4}}
                        gap={6}>
                {tasks.map((task, index) => (
                    <TaskCard key={index} task={task}/>
                ))}
            </SimpleGrid>
        </>
    );
};

export default TaskGroup;