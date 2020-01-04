import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { VALIDATE_GAME_AUTHENTICATION_FORM } from "forms";
import { compose } from "recompose";
import { Game } from "types";
import { validateGameAuthentication, loadGames } from "store/actions/game";
import {
  GameAuthenticationActions,
  VALIDATE_GAME_AUTHENTICATION_SUCCESS,
  VALIDATE_GAME_AUTHENTICATION_FAIL
} from "store/actions/game/types";
import { toastr } from "react-redux-toastr";
import authorize from "utils/oidc/AuthorizeService";

interface FormData {}

interface OutterProps {
  game: Game;
  handleCancel: () => any;
  setAuthenticationFactor: (data: any) => any;
}

type InnerProps = InjectedFormProps<FormData, Props> & {
  stripe: stripe.Stripe;
};

type Props = InnerProps & OutterProps;

const ReduxForm: FunctionComponent<Props> = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit}>
    <Button.Submit>Validate</Button.Submit>
  </Form>
);

const enhance = compose<any, any>(
  reduxForm<FormData, Props>({
    form: VALIDATE_GAME_AUTHENTICATION_FORM,
    onSubmit: async (
      values,
      dispatch: any,
      { game, setAuthenticationFactor }
    ) => {
      return await dispatch(validateGameAuthentication(game)).then(
        (action: GameAuthenticationActions) => {
          switch (action.type) {
            case VALIDATE_GAME_AUTHENTICATION_SUCCESS: {
              return dispatch(loadGames()).then(() => {
                console.log(window.location.pathname);
                return authorize
                  .getUser()
                  .then(user => console.log(user))
                  .then(() =>
                    authorize
                      .signIn({
                        returnUrl: window.location.pathname
                      })
                      .then(() => authorize.getUser().then(x => console.log(x)))
                  );
              });
            }
            case VALIDATE_GAME_AUTHENTICATION_FAIL: {
              toastr.error("Error", "Validating game authentication failed.");
              setAuthenticationFactor(null);
              break;
            }
          }
          return Promise.resolve(action);
        }
      );
    }
  })
);

export default enhance(ReduxForm);
