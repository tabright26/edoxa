import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { GENERATE_GAME_AUTH_FACTOR_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Form/Validation";

const GenerateGameAuthFactorForm: FunctionComponent<any> = ({
  handleSubmit,
  generateGameAuthFactor,
  error,
  setAuthFactor
}) => (
  <Form
    onSubmit={handleSubmit((data: any) =>
      generateGameAuthFactor(data).then(action => {
        console.log(action);
        return setAuthFactor(action.payload.data);
      })
    )}
  >
    {error && <FormValidation error={error} />}
    <FormGroup>
      <label>Summoner name</label>
      <Field name="summonerName" component={Input.Text} />
    </FormGroup>
    <FormGroup>
      <label>Region</label>
      <Field
        name="region"
        component={Input.Select}
        disabled={true}
      >
        <option value="NA">North America</option>
      </Field>
    </FormGroup>
    <Button.Submit size="sm">Search</Button.Submit>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm({ form: GENERATE_GAME_AUTH_FACTOR_FORM, validate })
);

export default enhance(GenerateGameAuthFactorForm);
