import { FormControl } from "@angular/forms";

export interface GroupForm {
    user: FormControl<string>;
    group: FormControl<string>;
}