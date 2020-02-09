import React, { FunctionComponent } from "react";
import Moment from "react-moment";

type Props = {
  readonly currentState: string;
  readonly state: string;
  readonly unixTimeSeconds: number;
};

const Item: FunctionComponent<Props> = ({
  currentState,
  state,
  unixTimeSeconds
}) => {
  const isActiveState = currentState.toLowerCase() === state.toLowerCase();
  return (
    <span
      className={`btn btn-block btn-sm text-light mt-2 ${
        isActiveState ? "bg-primary" : "bg-secondary"
      }`}
      title={`${state}${isActiveState ? " (current)" : ""}`}
    >
      {unixTimeSeconds ? (
        <Moment unix format="lll">
          {unixTimeSeconds}
        </Moment>
      ) : (
        <span>...</span>
      )}
    </span>
  );
};

export default Item;
