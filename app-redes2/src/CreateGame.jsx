import React, { useState } from "react";

const CreateGame = () => {

    const [name, setName] = useState('');
    const [owner, setOwner] = useState('');
    const [password, setPassword] = useState('');


    function handleSubmit(e) {
        e.preventDefault();
        const game = {name,owner,password}
        console.log(name)
        console.log(owner)
        console.log(password)

        fetch('https://virtserver.swaggerhub.com/UCR-SA/contaminaDOS/1.0.0/api/games' , {
            method: 'POST',
            headers: { "Content-Type": "application/json"},
            body: JSON.stringify(game)
        }).then(() => {
            console.log('create game succesfully')
        })
    }

    return (
        <div>
            <form onSubmit={handleSubmit}>
                <div className="createGame">
                    <label>Name of the game</label>
                    <input value={name} onChange={e => setName(e.target.value)} type="text"></input>
                    <label>Owner of the game</label>
                    <input value={owner} onChange={e => setOwner(e.target.value)} type="text"></input>
                    <label>Password</label>
                    <input value={password} onChange={e => setPassword(e.target.value)} type="password"></input>
                    <button type="submit">Create Game</button>
                </div>
            </form>
        </div>
    );
}

export default CreateGame;