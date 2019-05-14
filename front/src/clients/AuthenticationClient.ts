export default class AuthenticationClients {

    baseAddress: string;
    AuthenticationClients(baseAddress: string){
        this.baseAddress = baseAddress;
    }
    
    async getUser(username: string){
        var res = await fetch(this.baseAddress + "/users/" + username, {
            method: "GET",
        });

        if (res.ok) {
            var json = await res.json();
            return json as UserVM;
        }

        console.log(`ERROR: ${res}`);
        return null;
    }
    
    async registerUser(user: UserVM){
        var newUser = {
            id: "",
            username: user.username,
            email: user.email,
            password: user.password
        };

        var res = await fetch(this.baseAddress + "/users", {
            method: "POST", 
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newUser)
        });

        if (res.ok) {
            var json = await res.json();
            return json as UserVM;
        }

        console.log(`ERROR: ${res}`);
        return null;        
    }

    async loginUser(user: UserVM){
        var newUser = {
            id: "",
            username: user.username,
            password: user.password
        };

        var res = await fetch(this.baseAddress + "/login", {
            method: "POST", 
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify(newUser)
        });

        if (res.ok) {
            var json = await res.json();
            return json as UserVM;
        }

        console.log(`ERROR: ${res}`);
        return null; 
    }
}