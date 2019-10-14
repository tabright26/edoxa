const validate = values => {
  const line1RegExp = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
  const line2RegExp = new RegExp("^[a-zA-Z0-9- .,]{1,}$");
  const cityRegExp = new RegExp("^[a-zA-Z- ]{1,}$");
  const stateRegExp = new RegExp("^[a-zA-Z- ]{1,}$");
  const postalRegExp = new RegExp("^[0-9A-Z]{5,6}$");
  const errors: any = {};
  if (!values.line1) {
    errors.line1 = "Main address is required";
  } else if (!line1RegExp.test(values.line1)) {
    errors.line1 = "Invalid main address";
  }
  if (values.line2 && !line2RegExp.test(values.line2)) {
    errors.line2 = "Invalid secondary address";
  }
  if (!values.city) {
    errors.city = "City is required";
  } else if (!cityRegExp.test(values.city)) {
    errors.city = "Invalid city";
  }
  if (values.state && !stateRegExp.test(values.state)) {
    errors.state = "Invalid state";
  }
  if (values.postalCode && !postalRegExp.test(values.postalCode)) {
    errors.postalCode = "Invalid postal code";
  }
  return errors;
};

export default validate;
