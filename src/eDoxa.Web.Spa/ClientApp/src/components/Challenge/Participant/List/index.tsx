import React, { FunctionComponent, useState, useEffect } from "react";
import { connect, MapStateToProps } from "react-redux";
import Item from "./Item";
import { RootState } from "store/types";
import { ChallengeId, ChallengeParticipant } from "types";
import { RouteComponentProps, withRouter } from "react-router-dom";
import { compose } from "recompose";
import { Paginate } from "components/Shared/Paginate";

const pageSize = 10;

type Params = {
  readonly challengeId?: ChallengeId;
};

type OwnProps = RouteComponentProps<Params> &
  Params & {
    readonly payoutEntries: number;
  };

type StateProps = {
  readonly participants: ChallengeParticipant[];
  readonly payoutEntries: number;
  readonly bestOf: number;
};

type InnerProps = OwnProps & StateProps;

type OutterProps = Params;

type Props = InnerProps & OutterProps;

const List: FunctionComponent<Props> = ({
  participants,
  payoutEntries,
  bestOf
}) => {
  const [items, setItems] = useState([]);
  useEffect(() => {
    setItems(participants.slice(0, pageSize));
  }, [participants]);
  return (
    <>
      {items.map((participant, index) => (
        <Item
          key={index}
          participant={participant}
          position={participants.findIndex(x => x.id === participant.id) + 1}
          payoutEntries={payoutEntries}
          bestOf={bestOf}
        />
      ))}
      <Paginate
        className="my-4"
        pageSize={pageSize}
        totalItems={participants.length}
        onPageChange={(currentPage, pageSize) => {
          const start = currentPage * pageSize;
          const end = start + pageSize;
          setItems(participants.slice(start, end));
        }}
      />
    </>
  );
};

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
    bestOf: challenge.bestOf,
    payoutEntries: challenge.payout.entries,
    participants: challenge.participants
  };
};

const enhance = compose<InnerProps, OutterProps>(
  withRouter,
  connect(mapStateToProps)
);

export default enhance(List);
