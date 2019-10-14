const validate = values => {
  var nameRegExp = new RegExp("^[a-zA-Z0-9- .,]{3,20}$");
  const errors: any = {};
  if (!values.name) {
    errors.name = "Name is required";
  } else if (!nameRegExp.test(values.name)) {
    errors.name = "Invalid format. Must between 3-20 characters and alphanumeric. Hyphens, spaces, dot and coma allowed.";
  }
  return errors;
};

export default validate;
