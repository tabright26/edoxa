import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import FormField from "components/Shared/Form/Field";
import { CREATE_USER_ADDRESS_FORM } from "forms";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { createUserAddress } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  countryRegex,
  line1Regex,
  line2Regex,
  cityRegex,
  stateRegex,
  postalRegex,
  COUNTRY_REQUIRED,
  COUNTRY_INVALID,
  LINE1_REQUIRED,
  LINE1_INVALID,
  LINE2_INVALID,
  CITY_REQUIRED,
  CITY_INVALID,
  STATE_INVALID,
  POSTAL_INVALID
} from "validation";
import { AxiosActionCreatorMeta } from "utils/axios/types";

interface FormData {
  country: string;
  line1: string;
  line2: string;
  city: string;
  state: string;
  postalCode: string;
}

interface OutterProps {
  handleCancel: () => void;
}

type InnerProps = InjectedFormProps<FormData, Props>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({
  handleSubmit,
  error,
  handleCancel
}) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormGroup>
      <FormField.Country />
    </FormGroup>
    <Field
      type="text"
      name="line1"
      label="Address line 1"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="line2"
      label="Address line 2 (optional)"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <Field
      type="text"
      name="city"
      label="City"
      formGroup={FormGroup}
      component={Input.Text}
    />
    <FormGroup row className="my-0">
      <Col xs="8">
        <Field
          type="text"
          name="state"
          label="State"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
      <Col xs="4">
        <Field
          type="text"
          name="postalCode"
          label="Postal Code"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
    </FormGroup>
    <FormGroup className="mb-0">
      <Button.Save className="mr-2" />
      <Button.Cancel onClick={() => handleCancel()} />
    </FormGroup>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: CREATE_USER_ADDRESS_FORM,
    onSubmit: async (values, dispatch) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(createUserAddress(values, meta));
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitSuccess: (result, dispatch, { handleCancel }) => handleCancel(),
    validate: values => {
      const errors: FormErrors<FormData> = {};
      if (!values.country) {
        errors.country = COUNTRY_REQUIRED;
      } else if (!countryRegex.test(values.country)) {
        errors.country = COUNTRY_INVALID;
      }
      if (!values.line1) {
        errors.line1 = LINE1_REQUIRED;
      } else if (!line1Regex.test(values.line1)) {
        errors.line1 = LINE1_INVALID;
      }
      if (values.line2 && !line2Regex.test(values.line2)) {
        errors.line2 = LINE2_INVALID;
      }
      if (!values.city) {
        errors.city = CITY_REQUIRED;
      } else if (!cityRegex.test(values.city)) {
        errors.city = CITY_INVALID;
      }
      if (values.state && !stateRegex.test(values.state)) {
        errors.state = STATE_INVALID;
      }
      if (values.postalCode && !postalRegex.test(values.postalCode)) {
        errors.postalCode = POSTAL_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
