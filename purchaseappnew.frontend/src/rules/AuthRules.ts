import { IRule } from '@/types/Rule';
import { required } from "vee-validate/dist/rules";

export default class AuthRules {

    getAuthRules(): IRule[] {
        return [
            {
                ruleName: "required",
                ruleFunction: required,
                message: "{_field_} не может быть пустым.",
            }
        ];
    }
}