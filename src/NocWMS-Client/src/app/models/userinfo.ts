import { User } from "oidc-client";

export class UserInfo {
    constructor(user?: Partial<UserInfo>) {
        if (user) {
            this.id = user.id;
            this.username = user.username;
            this.email = user.email;
            this.avatar = user.avatar;
            this.role = user.role;
            this.isCompleted = user.isCompleted;
        }
    }

    public id: string;
    public username: string;
    public email: string;
    public avatar: string;
    public role: string;
    public isCompleted: boolean;
}