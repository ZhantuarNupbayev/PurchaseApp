import { IRule } from '@/types/Rule';
import { extend, setInteractionMode } from 'vee-validate';

export default class Validation {
    private readonly defaultMode: string = "eager";

    constructor() {
        setInteractionMode(this.defaultMode);
    }

    setRules(rules: IRule[]): void {
        rules.map(function(rule) {
            extend(rule.ruleName, {
                ...rule.ruleFunction,
                message: rule.message
            });
        });
    }

}