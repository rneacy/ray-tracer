#pragma once

#include "Vector3.h"
#include <iostream>

// Writes the translated colour to the supplied stream.
void Write_Colour(std::ostream& out, Colour pix_col) {
	out << static_cast<int>(255.999 * pix_col[0]) << " "
		<< static_cast<int>(255.999 * pix_col[1]) << " "
		<< static_cast<int>(255.999 * pix_col[2]) << "\n";
}