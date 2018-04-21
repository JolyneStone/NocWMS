export class Province {
    constructor(province?: Partial<Province>) {
        if (province) {
            this.name = province.name;
            this.selected = province.selected;
        }
    }

    public name: string;
    public selected: boolean;
}