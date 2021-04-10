import { IRule } from '@/types/Rule';
import {
  required,
  integer,
  min,
  is_not,
  max,
  regex,
} from "vee-validate/dist/rules";

export default class ProductRules {

    getProductRules(): IRule[] {
        return [
        {
            ruleName: "required",
            ruleFunction: required,
            message: "{_field_} не может быть пустым.",
        },
        {
            ruleName: "integer",
            ruleFunction: integer,
            message: "{_field_} должно быть числом.",
        },
        {
            ruleName: "is_not",
            ruleFunction: is_not,
            message: "{_field_} не должен быть равным нулю.",
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
        {
            ruleName: "regex",
            ruleFunction: regex,
            message: "Нельзя ставить нули в начале.",
        },
    ];
    }
}