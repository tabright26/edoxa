import React, { FunctionComponent, useEffect } from "react";
import { compose } from "recompose";
import { connect, MapDispatchToProps } from "react-redux";
import { loadChallengeHistory } from "store/actions/challenge";
import ChallengeList from "components/Challenge/List";

type OwnProps = {};

type DispatchProps = { loadChallengeHistory: () => void };

type InnerProps = DispatchProps;

type OutterProps = OwnProps;

type Props = InnerProps & OutterProps;

const ProfileChallengeHistory: FunctionComponent<Props> = ({
  loadChallengeHistory
}) => {
  useEffect((): void => {
    loadChallengeHistory();
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);
  return (
    <>
      <h5 className="text-uppercase my-4">CHALLENGE HISTORY</h5>
      <ChallengeList history />
    </>
  );
};

const mapDispatchToProps: MapDispatchToProps<DispatchProps, OwnProps> = (
  dispatch: any
) => {
  return {
    loadChallengeHistory: () => dispatch(loadChallengeHistory())
  };
};

const enhance = compose<InnerProps, OutterProps>(
  connect(null, mapDispatchToProps)
);

export default enhance(ProfileChallengeHistory);
