import React, { FunctionComponent } from "react";
import { FormGroup, Col, Form } from "reactstrap";
import { Field, reduxForm, InjectedFormProps, FormErrors } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import FormField from "components/Shared/Form/Field";
import { CREATE_USER_ADDRESS_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { createUserAddress } from "store/actions/identity";
import { throwSubmissionError } from "utils/form/types";
import {
  ADDRESS_COUNTRY_REGEXP,
  ADDRESS_LINE1_REGEXP,
  ADDRESS_LINE2_REGEXP,
  ADDRESS_CITY_REGEXP,
  ADDRESS_STATE_REGEXP,
  ADDRESS_POSTAL_CODE_REGEXP,
  ADDRESS_COUNTRY_REQUIRED,
  ADDRESS_COUNTRY_INVALID,
  ADDRESS_LINE1_REQUIRED,
  ADDRESS_LINE1_INVALID,
  ADDRESS_LINE2_INVALID,
  ADDRESS_CITY_REQUIRED,
  ADDRESS_CITY_INVALID,
  ADDRESS_STATE_INVALID,
  ADDRESS_POSTAL_CODE_INVALID
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
          label="State / Province"
          formGroup={FormGroup}
          component={Input.Text}
        />
      </Col>
      <Col xs="4">
        <Field
          type="text"
          name="postalCode"
          label="Zip / Postal code"
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
        errors.country = ADDRESS_COUNTRY_REQUIRED;
      } else if (!ADDRESS_COUNTRY_REGEXP.test(values.country)) {
        errors.country = ADDRESS_COUNTRY_INVALID;
      }
      if (!values.line1) {
        errors.line1 = ADDRESS_LINE1_REQUIRED;
      } else if (!ADDRESS_LINE1_REGEXP.test(values.line1)) {
        errors.line1 = ADDRESS_LINE1_INVALID;
      }
      if (values.line2 && !ADDRESS_LINE2_REGEXP.test(values.line2)) {
        errors.line2 = ADDRESS_LINE2_INVALID;
      }
      if (!values.city) {
        errors.city = ADDRESS_CITY_REQUIRED;
      } else if (!ADDRESS_CITY_REGEXP.test(values.city)) {
        errors.city = ADDRESS_CITY_INVALID;
      }
      if (values.state && !ADDRESS_STATE_REGEXP.test(values.state)) {
        errors.state = ADDRESS_STATE_INVALID;
      }
      if (values.postalCode && !ADDRESS_POSTAL_CODE_REGEXP.test(values.postalCode)) {
        errors.postalCode = ADDRESS_POSTAL_CODE_INVALID;
      }
      return errors;
    }
  })
);

export default enhance(CustomForm);
