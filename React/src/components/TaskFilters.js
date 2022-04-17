import React from 'react';
import {Button, ButtonGroup, Select, Text} from "@chakra-ui/react";

const TaskFilters = () => {
    return (
        <>
        <Select
            position={'absolute'}
            right={10}
            top={100}
            width='30%'
            display={{sm: 'flex', md: 'none'}}
            variant='filled'
        >
            <option>All Tasks</option>
            <option>Incomplete Tasks</option>
            <option>Complete Tasks</option>
        </Select>

        <ButtonGroup
            display={{sm: 'none', md: 'flex'}}
            justifyContent='right'
            mt={-8}
            mr={{sm: 10, md: 18, lg: 20}}>
            <Text mt={1}>Filter: </Text>
            <Button isActive size='sm'>All</Button>
            <Button size='sm'>Incomplete</Button>
            <Button size='sm'>Complete</Button>
        </ButtonGroup>

</>
)
    ;
};

export default TaskFilters;