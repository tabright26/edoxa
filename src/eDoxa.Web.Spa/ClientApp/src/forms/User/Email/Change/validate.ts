const validate = values => {
  const emailRegExp = new RegExp("^([A-Z|a-z|0-9](\\.|_){0,1})+[A-Z|a-z|0-9]\\@([A-Z|a-z|0-9])+((\\.){0,1}[A-Z|a-z|0-9]){2}\\.[a-z]{2,3}$");
  const errors: any = {};
  if (!values.email) {
    errors.email = "Email is required";
  } else if (!emailRegExp.test(values.email)) {
    errors.email = "Invalid email";
  }
  return errors;
};

export default validate;
