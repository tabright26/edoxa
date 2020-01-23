import React, { FunctionComponent } from "react";
import { Card } from "reactstrap";
import ChallengeForm from "components/Challenge/Form";
import {
  HocUserIsAuthenticatedStateProps,
  withUserIsAuthenticated,
  HocUserProfileUserIdStateProps,
  withUserProfileUserId
} from "utils/oidc/containers";
import { compose } from "recompose";
import { ChallengeId, UserId, CHALLENGE_STATE_INSCRIPTION } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { MapStateToProps, connect } from "react-redux";
import { RootState } from "store/types";

interface Params {
  readonly challengeId: ChallengeId;
}

interface OwnProps extends RouteComponentProps<Params> {
  readonly userId: UserId;
}

interface StateProps {
  readonly canRegister: boolean;
}

type InnerProps = HocUserIsAuthenticatedStateProps &
  HocUserProfileUserIdStateProps &
  StateProps;

type OutterProps = {
  readonly className?: string;
};

type Props = InnerProps & OutterProps;

const ChallengeRegister: FunctionComponent<Props> = ({
  className,
  userId,
  isAuthenticated,
  canRegister
}) =>
  canRegister &&
  isAuthenticated && (
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
    challenge => challenge.id === ownProps.match.params.challengeId
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
  withUserIsAuthenticated,
  withUserProfileUserId,
  connect(mapStateToProps)
);

export default enhance(ChallengeRegister);
