import React, { FunctionComponent } from "react";
import { faDiscord } from "@fortawesome/free-brands-svg-icons";
import { REACT_APP_DISCORD_URL } from "keys";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { SizeProp } from "@fortawesome/fontawesome-svg-core";

type Props = {
  size?: SizeProp;
};

export const Discord: FunctionComponent<Props> = ({ size, children }) => (
  <a href={REACT_APP_DISCORD_URL} target="_blank" rel="noopener noreferrer">
    {children ? children : <FontAwesomeIcon icon={faDiscord} size={size} />}
  </a>
);
