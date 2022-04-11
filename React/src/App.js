import React, {useState} from "react";
import { ChakraProvider, Flex} from "@chakra-ui/react";
import './styles/App.css';
import LoginForm from "./components/LoginForm";
import TaskList from "./components/TaskList";
import Navbar from "./components/Navbar";


function App() {
    const [user, setUser] = useState(null);



    return (
        <ChakraProvider>

                {!user
                    ? <LoginForm setUser={setUser} />
                    : (
                        <>
                            <Navbar/>
                            <TaskList user={user} />
                        </>
                    )
                }

        </ChakraProvider>

    );
}

export default App;
