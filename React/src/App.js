import React, {useState} from "react";
import { ChakraProvider, Flex} from "@chakra-ui/react";

import LoginForm from "./components/LoginForm";


function App() {
    const [user, setUser] = useState(null);



    return (
        <ChakraProvider>
            <Flex justifyContent='center'>
                {!user && <LoginForm user={user} setUser={setUser} />}
            </Flex>
        </ChakraProvider>

    );
}

export default App;
