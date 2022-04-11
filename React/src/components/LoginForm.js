import React, {useState} from 'react';
import {Button, Flex, Input, Stack, Text} from "@chakra-ui/react";
import login from "../services/login";
import Logo from "./Logo";

const LoginForm = ({setUser}) => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(false);

    const doLogin = async () => {
        await login({
            email, password,
        })
            .then(res => {
                setUser(res);
                setError(false);
            })
            .catch(err => {
                setError(true);
            });

    };
    return (
        <Flex justifyContent='center'>
        <Stack textAlign='center'
               mt='20vh'
               spacing={5}>
            <Logo />
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
    );
};

export default LoginForm;