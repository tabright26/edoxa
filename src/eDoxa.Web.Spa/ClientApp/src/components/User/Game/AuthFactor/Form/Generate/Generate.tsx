import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { GENERATE_GAME_AUTH_FACTOR_FORM } from "forms";
import { compose } from "recompose";
import { validate } from "./validate";
import FormValidation from "components/Shared/Form/Validation";

const GenerateGameAuthFactorForm: FunctionComponent<any> = ({ handleSubmit, generateGameAuthFactor, error }) => (
  <Form onSubmit={handleSubmit((data: any) => generateGameAuthFactor(data))}>
    {error && <FormValidation error={error} />}
    <Field name="summonerName" label="Summoner name" formGroup={FormGroup} component={Input.Text} />
    <Button.Save />
  </Form>
);

const enhance = compose<any, any>(reduxForm({ form: GENERATE_GAME_AUTH_FACTOR_FORM, validate }));

export default enhance(GenerateGameAuthFactorForm);
