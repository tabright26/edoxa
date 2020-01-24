import React, { FunctionComponent } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";
import {
  HocUserProfileUserIdStateProps,
  withUserProfileUserId
} from "utils/oidc/containers";
import { compose } from "recompose";
import { ChallengeId, CHALLENGE_STATE_INSCRIPTION } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params &
  HocUserProfileUserIdStateProps;

type StateProps = {
  readonly canRegister: boolean;
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params & {
  readonly className?: string;
};

type Props = InnerProps & OutterProps;

const Register: FunctionComponent<Props> = ({
  className,
  userId,
  canRegister
}) =>
  canRegister && (
    <Card className={className}>
      <ChallengeForm.Register userId={userId} />
    </Card>
  );

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data } = state.root.challenge;
  const challenge = data.find(
    challenge =>
      challenge.id ===
      (ownProps.match
        ? ownProps.match.params.challengeId
        : ownProps.challengeId)
  );
  return {
    canRegister:
      challenge.state === CHALLENGE_STATE_INSCRIPTION &&
      !challenge.participants.some(
        participant => participant.user.id === ownProps.userId
      )
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  withUserProfileUserId,
  connect(mapStateToProps)
);

export default enhance(Register);
