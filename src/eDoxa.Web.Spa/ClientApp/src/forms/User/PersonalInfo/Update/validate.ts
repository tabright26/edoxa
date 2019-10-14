const validate = values => {
  const firstNameRegExp = new RegExp("^[A-Z](((-)[A-Z])|[a-z]){1,15}$");
  const errors: any = {};
  if (!values.firstName) {
    errors.firstName = "First name is required";
  } else if (!firstNameRegExp.test(values.firstName)) {
    errors.firstName = "Invalid first name";
  }
  return errors;
};

export default validate;
