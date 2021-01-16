#include "ppm.h"
#include "Colour.h"

int main() {
	render();
}

void render() {
	const int i_width = 256;
	const int i_height = 256;

	// P3 denotes a Portable PixMap (RGB)
	// 3, 2 is the width and height of the image in pixels
	// 0-255 are byte values for each part of RGB
	std::cout << "P3\n" << i_width << " " << i_height << "\n255\n";

	// Render
	for (int i = 0; i < i_height; i++) {
		std::cerr << "\rRendering line " << i + 1 << " of " << i_height << std::flush;

		for (int j = 0; j < i_width; j++) {
			// Normalises these
			auto r = double(i) / (i_width - 1);
			auto g = double(j) / (i_height - 1);
			auto b = 0.25;

			Colour pix_col(r, g, b);
			Write_Colour(std::cout, pix_col);
		}
	}

	std::cerr << "\nRender complete.\n";
}