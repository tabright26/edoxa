import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { REGISTER_CHALLENGE_PARTICIPANT_FROM } from "utils/form/constants";
import { compose } from "recompose";
import { registerChallengeParticipant } from "store/actions/challenge";
import {
  ChallengesActions,
  REGISTER_CHALLENGE_PARTICIPANT_FAIL
} from "store/actions/challenge/types";
import { toastr } from "react-redux-toastr";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { ChallengeId, UserId } from "types";

interface FormData {}

interface OutterProps {
  userId: UserId;
}

type InnerProps = InjectedFormProps<FormData, Props> &
  RouteComponentProps<{ challengeId: ChallengeId }>;

type Props = InnerProps & OutterProps;

const CustomForm: FunctionComponent<Props> = ({ handleSubmit }) => (
  <Form onSubmit={handleSubmit} className="h-100">
    <Button.Submit
      color="primary"
      size="lg"
      className="text-uppercase h-100 w-100"
    >
      REGISTER
    </Button.Submit>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  reduxForm<FormData, Props>({
    form: REGISTER_CHALLENGE_PARTICIPANT_FROM,
    onSubmit: async (_values, dispatch: any, { match }) =>
      await dispatch(
        registerChallengeParticipant(match.params.challengeId)
      ).then((action: ChallengesActions) => {
        switch (action.type) {
          case REGISTER_CHALLENGE_PARTICIPANT_FAIL: {
            return toastr.error(
              "ERROR",
              "An error occurred while registering as a challenge participant."
            );
          }
          default: {
            return Promise.resolve();
          }
        }
      })
  })
);

export default enhance(CustomForm);
