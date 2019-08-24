const validate = values => {
  const errors = {};
  if (!values.firstName) {
    errors.firstName = "First name is required.";
  }
  if (!values.lastName) {
    errors.lastName = "Last name is required.";
  }
  return errors;
};

export default validate;
