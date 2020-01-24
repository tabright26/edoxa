import React, { FunctionComponent } from "react";
import { Row, Col } from "reactstrap";
import ChallengeItem from "./Item";
import { Loading } from "components/Shared/Loading";
import { compose } from "recompose";
import { connect, MapStateToProps } from "react-redux";
import { RootState } from "store/types";
import { Challenge } from "types";
import produce, { Draft } from "immer";
import {
  HocUserProfileUserIdStateProps,
  withUserProfileUserId
} from "utils/oidc/containers";

type OwnProps = HocUserProfileUserIdStateProps & { history?: boolean };

type StateProps = { challenges: Challenge[]; loading: boolean };

type InnerProps = StateProps;

type OutterProps = { history?: boolean };

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({ challenges, loading }) =>
  loading ? (
    <Loading />
  ) : (
    <Row>
      {challenges.map((challenge, index) => (
        <Col key={index} xs="12" sm="12" md="12" lg="12">
          <ChallengeItem challenge={challenge} />
        </Col>
      ))}
    </Row>
  );

const mapStateToProps: MapStateToProps<StateProps, OwnProps, RootState> = (
  state,
  ownProps
) => {
  const { data, loading } = state.root.challenge;
  if (ownProps.history) {
    return {
      challenges: produce(data, (draft: Draft<Challenge[]>) =>
        draft.filter(challenge =>
          challenge.participants.some(
            participant => participant.user.id === ownProps.userId
          )
        )
      ),
      loading
    };
  } else {
    return {
      challenges: data,
      loading
    };
  }
};

const enhance = compose<InnerProps, OutterProps>(
  withUserProfileUserId,
  connect(mapStateToProps)
);

export default enhance(List);
