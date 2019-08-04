import React from 'react';
import { Table, Card } from 'react-bootstrap';

import Spinner from '../Shared/Spinner';
import CurrencyFormat from '../Shared/Formaters/CurrencyFormat';
import BucketFormat from '../Shared/Formaters/BucketFormat';

const Render = ({ challenge }) => {
  if (!challenge) {
    return (
      <Card.Body>
        <Spinner />
      </Card.Body>
    );
  } else {
    let prevSize = 1;
    let nextSize = 0;
    return (
      <Table striped bordered hover size="sm" variant="dark" className="m-0">
        <thead>
          <tr>
            <th>Position</th>
            <th>Prize</th>
          </tr>
        </thead>
        <tbody>
          {/* {challenge.payout.buckets.map((bucket, index) => {
            const bucketSize = bucket.size;
            nextSize += bucketSize;
            const bucketRender = (
              <tr key={index}>
                <td>
                  { <BucketFormat prevSize={prevSize} nextSize={nextSize} /> }
                </td>
                <td>
                  { <CurrencyFormat
                    currency={challenge.payout.prizePool.currency}
                    amount={bucket.prize}
                  /> }
                </td>
              </tr>
            );
            prevSize += bucketSize;
            return bucketRender;
          })} */}
        </tbody>
      </Table>
    );
  }
};

const ChallengePayout = ({ challenge }) => {
  return (
    <Card bg="dark" className="my-4 text-light text-center">
      <Card.Header as="h5">
        {/* Payout ({challenge ? challenge.payout.prizePool.currency : ''}) */}
      </Card.Header>
      <Render challenge={challenge} />
    </Card>
  );
};

export default ChallengePayout;
