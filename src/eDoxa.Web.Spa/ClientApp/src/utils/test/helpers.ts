import { ReactWrapper } from "enzyme";

export function findFieldByName(
  wrapper: ReactWrapper,
  fieldName: string
): ReactWrapper {
  return wrapper.find(`Field[name="${fieldName}"]`).first();
}

export function findInputByName(wrapper: ReactWrapper, inputName: string): any {
  return wrapper.find(`input[name="${inputName}"]`).first();
}

export function findCancelButton(wrapper: ReactWrapper): any {
  return wrapper
    .find(`Cancel`)
    .first()
    .find(`button`)
    .first();
}

export function findLinkButton(wrapper: ReactWrapper): any {
  return wrapper
    .find(`Link`)
    .first()
    .find(`button`)
    .first();
}

export function findSubmitButton(wrapper: ReactWrapper): any {
  return wrapper
    .find(`Submit`)
    .first()
    .find(`button`)
    .first();
}

export function findFormFeedback(
  wrapper: ReactWrapper,
  message: string
): boolean {
  return wrapper
    .find(`FormFeedback`)
    .someWhere(node => node.text() === message);
}
