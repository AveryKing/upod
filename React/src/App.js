import React, {useEffect, useState} from "react";
import {Button, ChakraProvider, Flex, Input, Stack, Text} from "@chakra-ui/react";

const API_URL = 'https://localhost:7135/api';

function App() {
  //  const [list, setList] = useState([]);
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(false);

    const doLogin = () => {
        fetch(`${API_URL}/users/login`, {
            method: 'POST',
            headers: {'Content-Type': 'application/json'},
            body: JSON.stringify({email, password})
        })
            .then(res => res.json())
            .then(data => {
                if (data.token) {
                    setError(false)
                } else {
                    setError(true)
                }

                console.log(data);
            })
    }

    return (
        <ChakraProvider>
            <Flex justifyContent='center'>
                <Stack textAlign='center'
                       mt='25vh'
                       spacing={5}>

                    <Text
                        fontWeight='semibold'
                        fontSize='lg'>
                        Please enter your login credentials
                    </Text>
                    {error && (
                        <Text
                            mt={-10}
                            fontSize='sm'
                            color='#FF0000'>
                            The credentials you entered are invalid
                        </Text>
                    )}
                    <Input
                        onChange={(e) => setEmail(e.target.value)}
                        variant='filled'
                        placeholder='Email'/>

                    <Input onChange={(e) => setPassword(e.target.value)}
                           variant='filled'
                           type="password"
                           placeholder='Password'/>
                    <Button onClick={doLogin}>Login</Button>
                    <Button
                        fontSize='sm'
                        variant='link'>
                        Don't yet have an account?
                    </Button>
                </Stack>

            </Flex>
        </ChakraProvider>

    );
}

export default App;
