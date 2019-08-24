const validate = values => {
  const errors = {};
  if (!values.country) {
    errors.country = "Country is required.";
  }
  if (!values.line1) {
    errors.line1 = "Address line1 is required.";
  }
  return errors;
};

export default validate;
