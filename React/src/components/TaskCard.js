import React from 'react';
import {Flex, GridItem, Text} from "@chakra-ui/react";

const TaskCard = ({task}) => {
    return (
        <GridItem borderRadius={6} w='100%'  color='white' bg='teal.400'>
            <Flex margin={5} direction='column'>
                <Text>{task.name}</Text>
                <Text>{task.isComplete ? "Complete" : "Incomplete"}</Text>
            </Flex>

        </GridItem>
    );
};

export default TaskCard;