import React, { FunctionComponent } from "react";
import { Form } from "reactstrap";
import { reduxForm, InjectedFormProps } from "redux-form";
import Button from "components/Shared/Button";
import { REGISTER_CHALLENGE_PARTICIPANT_FROM } from "utils/form/constants";
import { compose } from "recompose";
import { registerChallengeParticipant } from "store/actions/challenge";
import { toastr } from "react-redux-toastr";
import { withRouter, RouteComponentProps } from "react-router-dom";
import { AxiosActionCreatorMeta } from "utils/axios/types";
import { throwSubmissionError } from "utils/form/types";
import { ChallengeId } from "types/challenges";
import { UserId } from "types/identity";

type FormData = {};

type InnerProps = InjectedFormProps<FormData, Props> &
  RouteComponentProps<{ challengeId: ChallengeId }>;

type OutterProps = {
  className?: string;
  userId: UserId;
};

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  handleSubmit,
  submitting,
  className
}) => (
  <Form onSubmit={handleSubmit} className="h-100">
    <Button.Submit color="primary" className={className} loading={submitting}>
      REGISTER
    </Button.Submit>
  </Form>
);

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  reduxForm<FormData, Props>({
    form: REGISTER_CHALLENGE_PARTICIPANT_FROM,
    onSubmit: async (_values, dispatch, { match }) => {
      try {
        return await new Promise((resolve, reject) => {
          const meta: AxiosActionCreatorMeta = { resolve, reject };
          dispatch(
            registerChallengeParticipant(match.params.challengeId, meta)
          );
        });
      } catch (error) {
        throwSubmissionError(error);
      }
    },
    onSubmitFail: () => {
      toastr.error(
        "ERROR",
        "An error occurred while registering as a challenge participant."
      );
    }
  })
);

export default enhance(Register);
