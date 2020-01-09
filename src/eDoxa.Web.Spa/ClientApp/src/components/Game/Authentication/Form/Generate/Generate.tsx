import React, { FunctionComponent } from "react";
import { Form, FormGroup } from "reactstrap";
import { Field, reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import Input from "components/Shared/Input";
import { GENERATE_GAME_AUTHENTICATION_FORM } from "utils/form/constants";
import { compose } from "recompose";
import FormValidation from "components/Shared/Form/Validation";
import { Game } from "types";
import { generateGameAuthentication } from "store/actions/game";
import {
  GameAuthenticationActions,
  GENERATE_GAME_AUTHENTICATION_FAIL,
  GENERATE_GAME_AUTHENTICATION_SUCCESS
} from "store/actions/game/types";
import { throwSubmissionError } from "utils/form/types";

interface FormData {}

interface OutterProps {
  game: Game;
  setAuthenticationFactor: (data: any) => any;
}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit, error }) => (
  <Form onSubmit={handleSubmit}>
    {error && <FormValidation error={error} />}
    <FormGroup>
      <label>Summoner name</label>
      <Field name="summonerName" component={Input.Text} />
    </FormGroup>
    <FormGroup>
      <label>Region</label>
      <Field name="region" component={Input.Select} disabled={true}>
        <option value="NA">North America</option>
      </Field>
    </FormGroup>
    <Button.Submit size="sm">Search</Button.Submit>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  reduxForm<FormData, Props>({
    form: GENERATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (
      values,
      dispatch: any,
      { game, setAuthenticationFactor }
    ) => {
      return await dispatch(generateGameAuthentication(game, values)).then(
        (action: GameAuthenticationActions) => {
          switch (action.type) {
            case GENERATE_GAME_AUTHENTICATION_SUCCESS: {
              setAuthenticationFactor(action.payload.data);
              break;
            }
            case GENERATE_GAME_AUTHENTICATION_FAIL: {
              throwSubmissionError(action.error);
              break;
            }
          }
          return Promise.resolve(action);
        }
      );
    }
  })
);

export default enhance(CustomForm);
