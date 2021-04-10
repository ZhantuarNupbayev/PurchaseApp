import { IRule } from '@/types/Rule';
import { required, min, max } from "vee-validate/dist/rules";

export default class CategoryRules {

    getCategoryRules(): IRule[] {
        return [
            {
                ruleName: "required",
                ruleFunction: required,
                message: "{_field_} не может быть пустым.",
            },
            {
                ruleName: "min",
                ruleFunction: min,
                message: "Слишком мало символов в {_field_}.",
            },
            {
                ruleName: "max",
                ruleFunction: max,
                message: "Слишком много символов в {_field_}.",
            },
        ];
    }
}