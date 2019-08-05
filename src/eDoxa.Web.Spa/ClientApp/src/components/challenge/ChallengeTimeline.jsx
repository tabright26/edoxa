import React from 'react';
import { Card } from 'react-bootstrap';

import Moment from 'react-moment';
import Spinner from '../Shared/Spinner';

import faker from 'faker';

faker.seed(1);

const style = { width: '200px' };

const Timeline = ({ challenge, state, date }) => {
  const isActiveState = challenge.state === state;
  return (
    <span
      className={`btn ${
        isActiveState ? 'bg-info' : 'bg-secondary'
      } text-light mt-2 rounded-0`}
      style={style}
      title={`${state}${isActiveState ? ' (current)' : ''}`}
    >
      {date ? (
        <Moment unix format="lll">
          {date}
        </Moment>
      ) : (
        <span>--</span>
      )}
    </span>
  );
};

const Body = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body className="text-center text-white">
        <Spinner />
      </Card.Body>
    );
  } else {
    return (
      <div
        className="d-flex flex-column position-relative"
        style={{
          right: '50px'
        }}
      >
        <span
          className="btn bg-primary text-light mt-2 rounded-0"
          style={style}
        >
          <strong>Timeline</strong>
        </span>
        <Timeline
          challenge={challenge}
          state="Inscription"
          date={challenge.timeline.createdAt}
        />
        <Timeline
          challenge={challenge}
          state="Started"
          date={challenge.timeline.startedAt}
        />
        <Timeline
          challenge={challenge}
          state="Ended"
          date={challenge.timeline.endedAt}
        />
        <Timeline
          challenge={challenge}
          state="Closed"
          date={challenge.timeline.closedAt}
        />
      </div>
    );
  }
};

const ChallengeTimeline = ({ challenge }) => <Body challenge={challenge} />;

export default ChallengeTimeline;