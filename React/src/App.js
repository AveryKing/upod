import React, {useState} from "react";
import { ChakraProvider, Flex} from "@chakra-ui/react";

import LoginForm from "./components/LoginForm";
import TaskList from "./components/TaskList";


function App() {
    const [user, setUser] = useState(null);



    return (
        <ChakraProvider>
            <Flex justifyContent='center'>
                {!user
                    ? <LoginForm setUser={setUser} />
                    : <TaskList user={user} />
                }
            </Flex>
        </ChakraProvider>

    );
}

export default App;
